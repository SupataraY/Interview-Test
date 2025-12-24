export interface UserListModel {
  id: string;
  userId: string;
  username: string;
  firstName: string;
  lastName: string;
  age: number | null;
  roleCount: number;
  permissionCount: number;
  //roles: RoleModel[];
  //permissions: string[];
}

export interface RoleModel {
  roleId: number;
  roleName: string;
}
