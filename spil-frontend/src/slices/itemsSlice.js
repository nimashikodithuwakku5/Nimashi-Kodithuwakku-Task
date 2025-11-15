import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchItems = createAsyncThunk("items/fetch", async () => {
  const res = await api.get("/items");
  return res.data;
});

const itemsSlice = createSlice({
  name: "items",
  initialState: { list: [], status: "idle" },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchItems.pending, (s) => { s.status = "loading"; })
      .addCase(fetchItems.fulfilled, (s, a) => { s.status = "succeeded"; s.list = a.payload; })
      .addCase(fetchItems.rejected, (s) => { s.status = "failed"; });
  }
});

export default itemsSlice.reducer;
