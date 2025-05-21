import { PropsWithChildren, useState } from "react";
import { AuthContext } from "./AuthContext";
import { EmployeeResponse } from "../../employee/services/EmployeeService";
import { Permission } from "../services/PermissionsService";

export const AuthContextProvider = (props: PropsWithChildren) => {
  const [user, setUser] = useState<string>();
  const [accessToken, setAccessToken] = useState<string>();
  const [employee, setEmployee] = useState<EmployeeResponse>();
  const [permissions, setPermissions] = useState<Permission[]>();

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        accessToken,
        setAccessToken,
        employee,
        setEmployee,
        permissions,
        setPermissions,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};
