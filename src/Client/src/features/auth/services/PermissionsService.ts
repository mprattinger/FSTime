import { AxiosError, AxiosInstance } from "axios";
import {
  ApplicationError,
  ApplicationErrorGenFactory,
  ApplicationErrorProblemDetailsFactory,
  ProblemDetailsData,
} from "../../../common/Types";

export const GetPermissions = async (
  api: AxiosInstance
): Promise<Permission[] | ApplicationError> => {
  try {
    const response = await api.get<PermissionResponse[] | ProblemDetailsData>(
      "/permissions/my"
    );

    if (response.status === 400) {
      return ApplicationErrorProblemDetailsFactory(
        response.data as ProblemDetailsData
      );
    }

    if (response.status !== 200) {
      return ApplicationErrorGenFactory(
        "Get my permissions failed",
        "PermissionsService"
      );
    }

    return (response.data as PermissionResponse[]).map((p) => ({
      id: p.id,
      group: p.group,
      action: translateActionToString(p.action),
    })) as Permission[];
  } catch (error) {
    let msg = error as string;

    if (error instanceof AxiosError) {
      msg = error.message;
    }
    return ApplicationErrorGenFactory(msg, "PermissionsService");
  }
};

export interface PermissionResponse {
  group: string;
  action: number;
  id: string;
}

export interface Permission {
  group: string;
  action: string;
  id: string;
}

const translateActionToString = (action: number): string => {
  switch (action) {
    case 1:
      return "R";
    case 2:
      return "W";
    case 3:
      return "D";
    case 4:
      return "A";
    default:
      return "U";
  }
};

// const translateActionFromString = (actionString: string): number => {
//   switch (actionString) {
//     case "R":
//       return 1;
//     case "W":
//       return 2;
//     case "D":
//       return 3;
//     case "A":
//       return 4;
//     default:
//       return 0;
//   }
// };
