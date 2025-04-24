import { PropsWithChildren, useState } from "react";
import { AuthContext } from "./AuthContext";

export const AuthContextProvider = (props: PropsWithChildren) => {
  const [user, setUser] = useState<string>()
  const [accessToken, setAccessToken] = useState<string>()

  return (
    <AuthContext.Provider value={{
      user,
      setUser,
      accessToken,
      setAccessToken,
    }}>
      {props.children}
    </AuthContext.Provider>
  );
}