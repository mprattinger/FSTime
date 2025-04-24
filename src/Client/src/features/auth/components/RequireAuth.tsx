import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

export const RequireAuth = () => {
  const {accessToken} = useAuth()
  const location = useLocation();

  return (
    accessToken ? <Outlet /> : <Navigate to="/login" state={{ from: location }} replace />
  )
}
