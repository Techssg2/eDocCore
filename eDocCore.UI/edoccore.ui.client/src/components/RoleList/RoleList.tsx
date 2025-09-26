import React, { useState } from 'react';
import './RoleList.css';

interface Role {
  id: number;
  name: string;
}

const RoleList: React.FC = () => {
  const [roles, setRoles] = useState<Role[]>([]);
  const [newRole, setNewRole] = useState<string>('');

  const handleAddRole = () => {
    if (newRole.trim() === '') return;
    setRoles([...roles, { id: Date.now(), name: newRole }]);
    setNewRole('');
  };

  return (
    <div className="role-list">
      <h2>Role List</h2>
      <ul>
        {roles.map(role => (
          <li key={role.id}>{role.name}</li>
        ))}
      </ul>
      <div className="add-role">
        <input value={newRole} onChange={e => setNewRole(e.target.value)} placeholder="Add role" />
        <button onClick={handleAddRole}>Add</button>
      </div>
    </div>
  );
};

export default RoleList;
