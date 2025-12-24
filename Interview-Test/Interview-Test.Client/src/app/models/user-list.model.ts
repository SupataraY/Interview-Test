export interface UserListModel {
  id: string;
  userId: string;
  username: string;
  firstName: string;
  lastName: string;
  age: number | null;
  roles: RoleModel[];
  permissions: string[];
}

export interface RoleModel {
  roleId: number;
  roleName: string;
}

export interface UserDetailModel {
  id: string;
  userId: string;
  username: string;
  firstName: string;
  lastName: string;
  age: number | null;
  roles: RoleModel[];
  permissions: string[];
}
