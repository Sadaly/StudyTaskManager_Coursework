import { Outlet, Link } from "react-router-dom";
import LogoutButton from "../Components/LogoutButton";

const HomeLayout = () => {
    return (
        <div style={{ display: 'flex', height: '100vh' }}>
            <nav style={{
                width: '15vw',
                padding: '1rem',
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'space-between', // Распределяет пространство между элементами
                height: '100%', // Занимает всю высоту родителя
            }}>
                <div>
                    <h2>Меню</h2>
                    <ul style={{
                        listStyle: 'none',
                        padding: 0
                    }}>
                        <li><Link to="/home">Главная</Link></li>
                        <li><Link to="/home/Chats">PersonalChats</Link></li>
                        <li><Link to="/home/groups">Groups</Link></li>
                    </ul>
                </div>
                <div style={{ marginTop: 'auto' }} >
                    <LogoutButton />
                </div>
            </nav>

            <div style={{
                width: '80vw',
                padding: '1rem',
            }}>
                <Outlet />
            </div>
        </div>
    );
};

export default HomeLayout;