import React from "react";

export default function FormInput({ label, children }) {
  return (
    <div className="mb-2">
      <label className="block text-sm text-gray-700 mb-1">{label}</label>
      {children}
    </div>
  );
}
