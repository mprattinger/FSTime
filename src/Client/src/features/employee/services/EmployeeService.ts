import { AxiosError, AxiosInstance } from "axios";
import {
  ApplicationError,
  ApplicationErrorGenFactory,
  ApplicationErrorProblemDetailsFactory,
  ProblemDetailsData,
} from "../../../common/Types";

export const GetMyEmployeeInfo = async (
  api: AxiosInstance
): Promise<EmployeeResponse | undefined | ApplicationError> => {
  try {
    const response = await api.get<EmployeeResponse | ProblemDetailsData>(
      "/employees/me"
    );

    if (response.status === 400) {
      if ((response.data as ProblemDetailsData).statusCode === 404) {
        return undefined;
      }
      return ApplicationErrorProblemDetailsFactory(
        response.data as ProblemDetailsData
      );
    }

    if (response.status !== 200) {
      return ApplicationErrorGenFactory(
        "Get Employee failed",
        "EmployeeService"
      );
    }

    return response.data as EmployeeResponse;
  } catch (error) {
    let msg = error as string;

    if (error instanceof AxiosError) {
      msg = error.message;
    }
    return ApplicationErrorGenFactory(msg, "LoginService");
  }
};

export interface EmployeeResponse {
  id: string;
  companyId: string;
  firstName: string;
  lastName: string;
  middleName: string;
  employeeCode: string;
  entryDate: string;
  user?: User;
  supervisor?: User;
  isHead: boolean;
  workschedules: Workschedule[];
}

export interface User {
  id: string;
  userName: string;
  email: string;
  verified: boolean;
}

export interface Workschedule {
  workscheduleId: string;
  from: string;
  to: string;
  description: string;
  monday: number;
  tuesday: number;
  wednesday: number;
  thursday: number;
  friday: number;
  saturday: number;
  sunday: number;
}
