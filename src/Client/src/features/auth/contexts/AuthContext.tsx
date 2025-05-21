import { createContext, Dispatch, SetStateAction } from "react";
import { EmployeeResponse } from "../../employee/services/EmployeeService";
import { Permission } from "../services/PermissionsService";

type AuthContextType = {
  user?: string;
  setUser: Dispatch<SetStateAction<string | undefined>>;
  accessToken?: string;
  setAccessToken: Dispatch<SetStateAction<string | undefined>>;
  employee?: EmployeeResponse;
  setEmployee: Dispatch<SetStateAction<EmployeeResponse | undefined>>;
  permissions?: Permission[];
  setPermissions: Dispatch<SetStateAction<Permission[] | undefined>>;
};

export const AuthContext = createContext<AuthContextType>(
  {} as AuthContextType
);
