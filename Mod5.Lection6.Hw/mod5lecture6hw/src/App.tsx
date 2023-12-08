import { Button, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import React from 'react';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import { routes } from './routes';

function App() {

  const theme = createTheme({
    palette: {
      primary: {
        light: "#63b8ff",
        main: "#0989e3",
        dark: "#005db0",
        contrastText: "#000",
      },
      secondary: {
        main: "#4db6ac",
        light: "#82e9de",
        dark: "#00867d",
        contrastText: "#000",
      },
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <BrowserRouter>
        <Layout>
            <Routes>
                {routes.map((route) => (
                  <Route
                  key={route.key}
                  path={route.path}
                  element={<route.component/>}
                  />
                ))}
            </Routes>
        </Layout>
      </BrowserRouter>
    </ThemeProvider>
  )

}

export default App;