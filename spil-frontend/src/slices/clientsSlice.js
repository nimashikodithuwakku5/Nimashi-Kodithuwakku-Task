import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchClients = createAsyncThunk("clients/fetch", async () => {
  const res = await api.get("/clients");
  return res.data;
});

const clientsSlice = createSlice({
  name: "clients",
  initialState: { list: [], status: "idle" },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchClients.pending, (s) => { s.status = "loading"; })
      .addCase(fetchClients.fulfilled, (s, a) => { s.status = "succeeded"; s.list = a.payload; })
      .addCase(fetchClients.rejected, (s) => { s.status = "failed"; });
  }
});

export default clientsSlice.reducer;
