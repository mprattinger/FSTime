import axios from "axios";

export const Api = axios.create({
  baseURL: "api",
});

export const SecureApi = axios.create({
  baseURL: "api",
  withCredentials: true,
});
