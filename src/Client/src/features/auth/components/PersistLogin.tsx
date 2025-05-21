import { useEffect, useState } from "react";
import { useRefreshToken } from "../hooks/useRefreshToken";
import { ApplicationError } from "../../../common/Types";
import { useAuth } from "../hooks/useAuth";
import { Outlet } from "react-router-dom";

export const PersistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const { user } = useAuth();

  const refresh = useRefreshToken();

  useEffect(() => {
    let isMounted = true;

    const verifyRefreshToken = async () => {
      const result = await refresh();
      if (result instanceof ApplicationError) {
        // Handle error (e.g., show a message to the user)
        console.error("Error refreshing token:", result.message);
      }

      if (isMounted) {
        setIsLoading(false);
      }
    };

    if (!user || user === "") {
      verifyRefreshToken();
    } else {
      setIsLoading(false);
    }

    return () => {
      isMounted = false;
    };
  }, [user, refresh]);

  return <>{isLoading ? <p>Loading...</p> : <Outlet />}</>;
};
