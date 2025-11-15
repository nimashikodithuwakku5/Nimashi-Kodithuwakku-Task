import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchOrders } from "../slices/ordersSlice";
import { useNavigate } from "react-router-dom";

export default function OrderListPage() {
  const dispatch = useDispatch();
  const orders = useSelector(s => s.orders?.list ?? []);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchOrders());
  }, [dispatch]);

  return (
    <div className="bg-white p-4 rounded shadow">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-semibold">Home</h2>
        <button onClick={()=>navigate("/order/new")} className="px-3 py-1 bg-white border border-gray-800 text-sm">Add New</button>
      </div>

      <div>
        <table className="w-full border-2 border-gray-800 table-fixed">
          <thead>
            <tr className="bg-gray-200">
              {Array.from({ length: 7 }).map((_, i) => (
                <th key={i} className="border border-gray-800 p-3 text-left text-sm">
                  <div className="flex items-center justify-between">
                    <span>{`Col ${i + 1}`}</span>
                    <span className="text-xs">▾</span>
                  </div>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {Array.from({ length: 6 }).map((_, r) => (
              <tr key={r}>
                {Array.from({ length: 7 }).map((__, c) => (
                  <td key={c} className="border border-gray-300 p-6 bg-white">&nbsp;</td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
