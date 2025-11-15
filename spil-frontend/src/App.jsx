import React from "react";
import { Routes, Route, Link } from "react-router-dom";
import OrderListPage from "./pages/OrderListPage";
import SalesOrderPage from "./pages/SalesOrderPage";



export default function App() {
  return (
    <div className="min-h-screen bg-gray-50 p-4">
      <header className="mb-4">
        <nav className="flex gap-4">
          <Link to="/" className="px-3 py-1 bg-white border rounded">Orders</Link>
          <Link to="/order/new" className="px-3 py-1 bg-white border rounded">New Order</Link>
        </nav>
      </header>

      <main>
        <Routes>
          <Route path="/" element={<OrderListPage />} />
          <Route path="/order/new" element={<SalesOrderPage />} />
          <Route path="/order/:id" element={<SalesOrderPage />} />
        </Routes>
      </main>
    </div>
  );
}
