export interface User {
  sysId: number;
  userName: string;
  email: string;
  createdAt: Date;
}

export interface PaginatedUserResponse {
  totalCount: number;
  page: number;
  pageSize: number;
  users: User[];
}
