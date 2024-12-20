import './SidebarMenu.css';

const SidebarMenu = ({ menuItems, active, onSelect }) => {
  return (
    <div className="sidebar">
      {menuItems.map((item, index) => (
        <div
          key={index}
          className={`menu-item ${item.name === active ? 'active' : ''}`}
          onClick={() => onSelect(item.name)}
        >
          <span>{item.name}</span>
        </div>
      ))}
    </div>
  );
};

export default SidebarMenu;