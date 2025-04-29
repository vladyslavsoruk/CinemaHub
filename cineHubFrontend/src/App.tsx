import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Navbar from "./components/layout/Navbar/Navbar.tsx";
import Footer from "./components/layout/Footer";
import MainPage from "./pages/MainPage";
import { CssBaseline, ThemeProvider, useMediaQuery } from "@mui/material";
import LoginPage from "./pages/loginPage/LoginPage.tsx";
import { useAppSelector } from "./hooks/storeHooks.ts";
import { getTheme } from "./store/slices/theme.ts";
import AboutMoviePage from "./pages/aboutMovie/AboutMoviePage.js";
import HomePage from "./pages/HomePage.tsx";
import AdminPanelPage from "./pages/AdminPanelPage.tsx";
import AdminHomePage from "./pages/AdminHomePage.tsx";
import StatisticsPage from "./pages/StatisticsPage.tsx";
import SeatBookingPage from "./pages/seatBooking/SeatBookingPage.tsx";
import SchedulePage from "./pages/SchedulePage.tsx";
import SearchPage from "./pages/SearchPage.tsx";
import TicketPage from "./pages/TicketPage.tsx";

function App() {
  const { mode } = useAppSelector((state) => state.themeReducer);
  const { role, isLogged } = useAppSelector((state) => state.authReducer);
  const systemMode = useMediaQuery("(prefers-color-scheme: dark)")
    ? "dark"
    : "light";
  return (
    <ThemeProvider theme={getTheme(mode, systemMode)}>
      <CssBaseline />
      <Router>
        <div
          style={{
            minHeight: "100vh",
            display: "flex",
            flexDirection: "column",
            paddingTop: "64px",
          }}
        >
          <Navbar />
          <Routes>
            <Route path="/" element={<MainPage />} />
            {!isLogged && <Route path="/login" element={<LoginPage />} />}

            <Route path="/schedule" element={<SchedulePage />} />
            <Route path="/search" element={<SearchPage />} />
            <Route path="/movie/:id" element={<AboutMoviePage />} />
            <Route path="/cart/seatplan" element={<SeatBookingPage />} />
            {role === "User" && (
              <>
                <Route path="/home" element={<HomePage />} />
                <Route path="/profile" element={<div />} />
                <Route path="/tickets" element={<TicketPage />} />
              </>
            )}
            {role === "Admin" && (
              <>
                <Route path="/home" element={<AdminHomePage />} />
                <Route path="/admin-panel" element={<AdminPanelPage />} />
                <Route path="/statistics" element={<StatisticsPage />} />
                <Route path="/settings" element={<div />} />
              </>
            )}
            {isLogged ? (
              <Route path="*" element={<Navigate to="/home" />} />
            ) : (
              <Route path="*" element={<Navigate to="/" />} />
            )}
          </Routes>
          <Footer />
        </div>
      </Router>
    </ThemeProvider>
  );
}

export default App;
