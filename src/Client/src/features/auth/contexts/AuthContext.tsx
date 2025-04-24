import { createContext, Dispatch, SetStateAction } from "react";

type AuthContextType = {
  user?: string;
  setUser: Dispatch<SetStateAction<string | undefined>>;
  accessToken?: string;
  setAccessToken: Dispatch<SetStateAction<string | undefined>>;
}

export const AuthContext = createContext<AuthContextType>({} as AuthContextType);