import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite';

// https://vite.dev/config/
// export default defineConfig(({ mode }) => {
//   const env = loadEnv(mode, process.cwd(), '');


//   plugins: [react()],
//   server: {
//     port: port: parseInt(env.PORT),
//   }
// })

export default defineConfig((inp) => {
  const env = loadEnv(inp.mode, process.cwd(), '');

  return {
    plugins: [react(), tailwindcss()],
    server: {
      port: parseInt(env.PORT),
      proxy: {
        '/api': {
          target: process.env.services__Api__https__0 || process.env.services__Api__http__0,
          changeOrigin: true,
          secure: false,
        } 
      }
    }
  }
});