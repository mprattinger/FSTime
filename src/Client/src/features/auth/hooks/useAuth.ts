import { useContext } from "react";
import { AuthContext } from "../contexts/AuthContext";

export const useAuth = () => {
  const {user, setUser, accessToken, setAccessToken} =  useContext(AuthContext); 

  return {
    user,
    setUser,
    accessToken,
    setAccessToken
  }
}

