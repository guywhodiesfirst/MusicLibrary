import './AdminPage.css'
import { useState } from 'react';
import SidebarMenu from '../../Components/UI/SidebarMenu/SidebarMenu';
import Users from './Users/Users';
import Reviews from './Reviews/Reviews'
import Comments from './Comments/Comments'

export default function AdminPage() {
  const [activeItem, setActiveItem] = useState('Users');

  const menuItems = [
    { name: 'Users' },
    { name: 'Reviews' },
    { name: 'Comments' }
  ];

  const components = {
    //Products: <Products />,
    Users: <Users />,
    Reviews: <Reviews />,
    Comments: <Comments />,
    Analytics: <div>Analytics Content</div>,
    Settings: <div>Settings Content</div>,
  };

  return (
    <div className="admin-page">
      <SidebarMenu
        menuItems={menuItems}
        active={activeItem}
        onSelect={setActiveItem}
      />
      <div className="main-content">
        {components[activeItem]}
      </div>
    </div>
  );
}