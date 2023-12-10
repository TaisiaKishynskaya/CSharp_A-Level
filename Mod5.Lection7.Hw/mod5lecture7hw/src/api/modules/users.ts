import { INewUser } from "../../interfaces/users";
import apiClient from "../client";

export const getById = (id: string) => apiClient({
  path: `users/${id}`,
  method: 'GET'
})

export const getByPage = (page: number) => apiClient({
  path: `users?page=${page}`,
  method: 'GET'
})

export const create = (user: INewUser) => apiClient({
  path: `users`,
  method: 'POST',
  data: user,
});

export const update = (id: number, user: INewUser) => apiClient({
  path: `users/${id}`,
  method: 'PUT',
  data: user,
});