import { AxiosError } from "axios";
import { useAuth } from "./useAuth";
import {
  ApplicationError,
  ApplicationErrorGenFactory,
} from "../../../common/Types";
import { SecureApi } from "../../../common/api/Api";

export const useRefreshToken = () => {
  const { setUser, setAccessToken } = useAuth();

  const refresh = async (): Promise<IRefreshResponse | ApplicationError> => {
    try {
      const resp = await SecureApi.get<IRefreshResponse>("/auth/refresh", {
        withCredentials: true,
      });

      setUser(resp.data.userName);
      setAccessToken(resp.data.accessToken);
      return resp.data;
    } catch (error) {
      let msg = error as string;

      if (error instanceof AxiosError) {
        msg = error.message;
      }
      return ApplicationErrorGenFactory(msg, "RefreshToken");
    }
  };

  return refresh;
};

export interface IRefreshResponse {
  userName: string;
  accessToken: string;
  accessTokenExpires: string;
}
