import { useState, useEffect } from 'react';

const API_URL = '/api/roles'; // ???ng d?n API, ch?nh l?i n?u c?n

function useRoles() {
  const [roles, setRoles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // L?y danh s�ch role
  const fetchRoles = async () => {
    setLoading(true);
    try {
      const res = await fetch(API_URL);
      const data = await res.json();
      setRoles(data);
      setError(null);
    } catch (err) {
      setError('L?i khi l?y danh s�ch role');
    }
    setLoading(false);
  };

  // Th�m role
  const addRole = async (role) => {
    try {
      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(role),
      });
      if (res.ok) fetchRoles();
    } catch (err) {
      setError('L?i khi th�m role');
    }
  };

  // S?a role
  const updateRole = async (role) => {
    try {
      const res = await fetch(`${API_URL}/${role.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(role),
      });
      if (res.ok) fetchRoles();
    } catch (err) {
      setError('L?i khi s?a role');
    }
  };

  // X�a role
  const deleteRole = async (id) => {
    try {
      const res = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE' });
      if (res.ok) fetchRoles();
    } catch (err) {
      setError('L?i khi x�a role');
    }
  };

  useEffect(() => {
    fetchRoles();
  }, []);

  return { roles, loading, error, addRole, updateRole, deleteRole };
}

export default useRoles;
