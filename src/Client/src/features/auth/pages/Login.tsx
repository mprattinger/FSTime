import clocks from "@/assets/clocks.png";
import { useEffect, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { LoginService } from "../services/LoginService";
import { useAuth } from "../hooks/useAuth";
import { ApplicationError } from "../../../common/Types";
import { GetMyEmployeeInfo } from "../../employee/services/EmployeeService";
import { GetPermissions } from "../services/PermissionsService";
import { useSecureApi } from "../hooks/useSecureApi";

export const Login = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { setUser, setEmployee, setPermissions, setAccessToken } = useAuth();
  const api = useSecureApi();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const usernameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);

  const from = location.state?.from?.pathname || "/";

  useEffect(() => {
    usernameRef.current?.focus();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      //Check credentials against api
      const result = await LoginService(username, password);
      if (result instanceof ApplicationError) {
        //If not ok, show error message
        return;
      }
      setUser(result.userName);
      setAccessToken(result.accessToken);

      //Eventuelle Employee infos laden
      const employee = await GetMyEmployeeInfo(api);
      if (employee) {
        if (employee instanceof ApplicationError) {
          //If not ok, show error message
          return;
        }
        setEmployee(employee);
      }

      //Nun die Berechtigungen laden
      const permissions = await GetPermissions(api);
      if (permissions instanceof ApplicationError) {
        return;
      }
      setPermissions(permissions);

      //If ok navigate to from
      setUsername("");
      setPassword("");
      navigate(from);
    } catch (error) {
      console.error("Login failed", error);
    }
  };

  return (
    <div className="w-full h-full absolute flex flex-col items-center bg-slate-200">
      <div className="text-8xl font-bold font-[caveat] flex flex-col items-center mt-6">
        Time
        <div className="w-64">
          <img src={clocks} alt="clocks" className="" />
        </div>
      </div>
      <div className="bg-white opacity-95 p-8 rounded-lg md:w-96 shadow-xl mt-6 w-80">
        <form onSubmit={handleSubmit} className="gap-y-4 flex flex-col">
          <div className="flex flex-col gap-y-2">
            <label htmlFor="username">Login</label>
            <input
              ref={usernameRef}
              id="username"
              type="text"
              placeholder="Username"
              className="border border-emerald-400 rounded p-2 outline-none"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div className="flex flex-col gap-y-2">
            <label htmlFor="password">Passwort</label>
            <input
              ref={passwordRef}
              id="password"
              type="password"
              placeholder="Password"
              className="border border-emerald-400 rounded p-2 outline-none"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <div className="w-full flex justify-end">
            <button className="border border-emerald-400 bg-emerald-300 py-2 px-1 min-w-28 rounded hover:bg-emerald-400 active:scale-95">
              Login
            </button>
          </div>
        </form>
      </div>
      <div className="absolute bottom-0 left-2">&copy; FlintSoft, 2025</div>
    </div>
  );
};
