import { Route, Routes } from "react-router-dom";
import { Login } from "./features/auth/pages/Login";
import { Layout } from "./common/components/Layout";
import { RequireAuth } from "./features/auth/components/RequireAuth";
import { MainLayout } from "./common/components/MainLayout";
import { PersistLogin } from "./features/auth/components/PersistLogin";
import { HomePage } from "./features/home/pages/HomePage";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route element={<PersistLogin />}>
          <Route element={<RequireAuth />}>
            <Route element={<MainLayout />}>
              <Route path="/" element={<HomePage />} />
            </Route>
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
