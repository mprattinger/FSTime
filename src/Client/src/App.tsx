import { Route, Routes } from "react-router-dom"
import { Login } from "./features/auth/pages/Login"
import { Layout } from "./common/components/Layout"
import { RequireAuth } from "./features/auth/components/RequireAuth"
import { MainLayout } from "./common/components/MainLayout"

function App() {
  
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path="login" element={<Login />} />
        <Route element={<RequireAuth />}>
        <Route path="/" element={<MainLayout />}>
        </Route>
        </Route>
      </Route>
    </Routes>
  ) 
}

export default App