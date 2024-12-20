import { useCallback, useEffect, useState } from 'react';
import './Users.css'
import UsersTable from './UsersTable.jsx';
import { UsersApi } from '../../../API/UsersApi.js';
import { AdminApi } from '../../../API/AdminApi.js';

export default function Users() {
  const [users, setUsers] = useState([]);
  
    const fetchUsers = async () => {
        try {
            const response = await UsersApi.getAll();
            if(!response.success) {
                alert(response.message)
            } else {
                setUsers(response.users)
            }
        } catch (error) {
            alert(error)
        }
    }

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleBlock = async (user) => {
    try {
        const response = await AdminApi.blockUser(user.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchUsers()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleUnblock = async (user) => {
    try {
        const response = await AdminApi.unblockUser(user.id);
        if(!response.success) {
            alert(response.message)
        } else {
            await fetchUsers()
        }
    } catch (error) {
        alert(error)
    }
  }

  const handleBlockButtonClick = async (user) => {
    if(user.isBlocked) {
        await handleUnblock(user)
    } else {
        await handleBlock(user)
    }
  }

  return (
    <>
      <UsersTable users={users} onBlock={handleBlockButtonClick} />
    </>
  );
}