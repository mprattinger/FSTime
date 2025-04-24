import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AuthContextProvider } from "./features/auth/contexts/AuthContextProvider.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <AuthContextProvider>
        <Routes>
          <Route path="*" element={<App />} />
        </Routes>
      </AuthContextProvider>
    </BrowserRouter>
  </StrictMode>
);
