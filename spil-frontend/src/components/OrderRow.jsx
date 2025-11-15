// src/components/OrderRow.jsx
import React from "react";

export default function OrderRow({ row, items, onChange }) {
  // items: [{ itemId, itemCode, description, price }, ...]
  return (
    <tr className="h-6">
      <td className="border border-black p-1">
        <select className="w-full border-0 text-xs p-0" value={row.itemId ?? ""} onChange={(e) => onChange("itemId", e.target.value)}>
          <option value="">--</option>
          {items.map(it => (
            <option key={it.itemId} value={it.itemId}>{it.itemCode}</option>
          ))}
        </select>
      </td>

      <td className="border border-black p-1">
        <select className="w-full border-0 text-xs p-0" value={row.description ?? ""} onChange={(e) => onChange("description", e.target.value)}>
          <option value="">--</option>
          {items.map(it => (
            <option key={it.itemId} value={it.description}>{it.description}</option>
          ))}
        </select>
      </td>

      <td className="border border-black p-1">
        <input type="text" className="w-full border-0 text-xs p-0" value={row.note ?? ""} onChange={(e) => onChange("note", e.target.value)} />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.quantity ?? 0} onChange={(e) => onChange("quantity", e.target.value)} min="0" />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.price ?? ""} readOnly />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.taxRate ?? 0} onChange={(e) => onChange("taxRate", e.target.value)} />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.excl ?? 0} readOnly />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.tax ?? 0} readOnly />
      </td>

      <td className="border border-black p-1 text-right">
        <input type="number" className="w-full border-0 text-xs p-0 text-right" value={row.incl ?? 0} readOnly />
      </td>
    </tr>
  );
}
