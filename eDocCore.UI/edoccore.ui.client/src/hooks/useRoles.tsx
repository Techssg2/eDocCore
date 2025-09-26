import { useState, useEffect } from 'react';

interface Role {
  id: number;
  name: string;
}

function useRoles() {
  const [roles, setRoles] = useState<Role[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    setLoading(true);
    setTimeout(() => {
      setRoles([
        { id: 1, name: 'Admin' },
        { id: 2, name: 'User' }
      ]);
      setLoading(false);
    }, 1000);
  }, []);

  return { roles, loading };
}

export default useRoles;
