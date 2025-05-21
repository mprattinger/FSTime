import { AxiosError } from "axios";
import {
  ApplicationError,
  ApplicationErrorGenFactory,
  ApplicationErrorProblemDetailsFactory,
  ProblemDetailsData,
} from "../../../common/Types";
import { Api } from "../../../common/api/Api";

export const LoginService = async (
  username: string,
  password: string
): Promise<LoginResult | ApplicationError> => {
  try {
    const response = await Api.post<LoginResponse | ProblemDetailsData>(
      "/auth/login",
      {
        username: username,
        password: password,
      }
    );

    if (response.status === 400) {
      return ApplicationErrorProblemDetailsFactory(
        response.data as ProblemDetailsData
      );
    }

    if (response.status !== 200) {
      return ApplicationErrorGenFactory("Login failed", "LoginService");
    }

    return response.data as LoginResult;
  } catch (error) {
    let msg = error as string;

    if (error instanceof AxiosError) {
      msg = error.message;
    }
    return ApplicationErrorGenFactory(msg, "LoginService");
  }
};

interface LoginResponse {
  userName: string;
  accessToken: string;
  accessTokenExpires: string;
  refereshToken: string;
  refreshTokenExpires: string;
}

interface LoginResult {
  userName: string;
  accessToken: string;
}
