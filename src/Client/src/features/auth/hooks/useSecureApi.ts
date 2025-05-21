import { useEffect } from "react";
import { useRefreshToken } from "./useRefreshToken";
import { SecureApi } from "../../../common/api/Api";
import { useAuth } from "./useAuth";

export const useSecureApi = () => {
  const refresh = useRefreshToken();
  const { accessToken } = useAuth();

  useEffect(() => {
    const requestIntercept = SecureApi.interceptors.request.use(
      (config) => {
        if (!config.headers["Authorizatio"]) {
          config.headers["Authorization"] = `Bearer ${accessToken}`;
        }
        return config;
      },
      (error) => Promise.reject(error)
    );

    const responseIntercept = SecureApi.interceptors.response.use(
      (response) => response,
      async (error) => {
        const prevRequest = error?.config;
        if (error?.response?.status === 401 && !prevRequest?.sent) {
          prevRequest.sent = true;
          const accessToken = await refresh();
          prevRequest.headers["Authorization"] = `Bearer ${accessToken}`;
          return SecureApi(prevRequest);
        }
        return Promise.reject(error);
      }
    );

    return () => {
      SecureApi.interceptors.request.eject(requestIntercept);
      SecureApi.interceptors.response.eject(responseIntercept);
    };
  }, [accessToken, refresh]);

  return SecureApi;
};
