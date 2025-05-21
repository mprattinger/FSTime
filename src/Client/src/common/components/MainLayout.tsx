import { Menu } from "lucide-react";
import logo from "../../assets/clockLogo.png";
import { useState } from "react";
import { Outlet } from "react-router-dom";

export const MainLayout = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  return (
    <div className="w-full h-full absolute bg-gradient-to-r from-blue-400 to-emerald-400">
      <header className="flex justify-between items-center text-black py-3 px-8 md:px-20 bg-white drop-shadow-md">
        <a href="/" className="flex items-center">
          <img
            src={logo}
            alt="Logo"
            className="w-14 hover:scale-105 transition-all"
          />
          <span className="ml-6 text-6xl font-bold font-[caveat]">Time</span>
        </a>

        <ul className="hidden xl:flex items-center gap-12 font-semibold text-base">
          <li className="p-3 hover:bg-sky-400 hover:text-white rounded-md transition-all cursor-pointer">
            Home
          </li>
          <li className="p-3 hover:bg-sky-400 hover:text-white rounded-md transition-all cursor-pointer">
            Products
          </li>
          <li className="p-3 hover:bg-sky-400 hover:text-white rounded-md transition-all cursor-pointer">
            Explore
          </li>
          <li className="p-3 hover:bg-sky-400 hover:text-white rounded-md transition-all cursor-pointer">
            Contact
          </li>
        </ul>

        <Menu
          className="xl:hidden block cursor-pointer"
          size={32}
          onClick={() => setIsMenuOpen((p) => !p)}
        />

        <div
          className={`absolute xl:hidden top-24 left-0 w-full bg-white flex flex-col items-center transform transitition-transform ${
            isMenuOpen ? "opacity-100" : "opacity-0"
          }`}
          style={{ transition: "transform 0.3s ease, opacity 0.3s ease" }}
        >
          <li className="list-none w-full text-center p-4 hover:bg-sky-400 hover:text-white transition-all cursor-pointer">
            Home
          </li>
          <li className="list-none w-full text-center p-4 hover:bg-sky-400 hover:text-white transition-all cursor-pointer">
            Products
          </li>
          <li className="list-none w-full text-center p-4 hover:bg-sky-400 hover:text-white transition-all cursor-pointer">
            Explore
          </li>
          <li className="list-none w-full text-center p-4 hover:bg-sky-400 hover:text-white transition-all cursor-pointer">
            Contact
          </li>
        </div>
      </header>
      <Outlet />
    </div>
  );
};
