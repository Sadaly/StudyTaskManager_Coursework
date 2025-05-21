import { Outlet, Link } from "react-router-dom";
import LogoutButton from "../Components/LogoutButton";

const HomeLayout = () => {
    return (
        <div style={{
            display: 'flex',
            maxWidth: '1200px',
            margin: '0 0 0 20px', // Прижимаем к левому краю с небольшим отступом
            height: '100vh'
        }}>
            <nav style={{
                width: '200px',
                padding: '1rem',
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
                borderRight: '1px solid #ccc'
            }}>
                <div>
                    <h2>Меню</h2>
                    <ul style={{
                        listStyle: 'none',
                        padding: 0
                    }}>
                        <li style={{ marginBottom: '10px' }}><Link to="/home">Главная</Link></li>
                        <li style={{ marginBottom: '10px' }}><Link to="/home/Chats">PersonalChats</Link></li>
                        <li style={{ marginBottom: '10px' }}><Link to="/home/groups">Groups</Link></li>
                    </ul>
                </div>
                <div style={{ marginTop: 'auto', marginBottom: '20px' }} >
                    <LogoutButton />
                </div>
            </nav>

            <div style={{
                flex: 1,
                padding: '1rem',
                overflowY: 'auto'
            }}>
                <Outlet />
            </div>
        </div>
    );
};

export default HomeLayout;