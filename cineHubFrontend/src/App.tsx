import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Navbar from "./components/layout/Navbar";
import MainPage from "./pages/MainPage";
import Footer from "./components/layout/Footer";

export const role: "Admin" | "User" = "User"; //for demo

function App() {
  return (
    <Router>
      <div
        style={{ minHeight: "100vh", display: "flex", flexDirection: "column" }}
      >
        <Navbar />
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/login" element={<div />} />

          <Route path="/schedule" element={<div />} />
          <Route path="/search" element={<div />} />
          <Route path="/movie/:id" element={<div />} />
          <Route path="/cart/seatplan" element={<div />} />
          {role === "User" && (
            <>
              <Route path="/home" element={<div />} />
              <Route path="/profile" element={<div />} />
              <Route path="/tickets" element={<div />} />
            </>
          )}
          {role === "Admin" && (
            <>
              <Route path="/home" element={<div />} />
              <Route path="/admin-panel" element={<div />} />
              <Route path="/statistics" element={<div />} />
              <Route path="/settings" element={<div />} />
            </>
          )}
        </Routes>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
