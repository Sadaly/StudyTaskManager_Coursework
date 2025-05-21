import { Outlet, Link } from "react-router-dom";
import LogoutButton from "../Components/LogoutButton";

const HomeLayout = () => {
    return (
        <div style={{ display: 'flex' }}>
            <nav style={{
                width: '15vw',
                height: '90vh',
                padding: '1rem',
            }}>
                <h2>Меню</h2>
                <ul
                    style={{
                        listStyle: 'none',
                        padding: 0
                    }}
                >
                    <li><Link to="/home">Главная</Link></li>
                    <li><Link to="/home/Chats">PersonalChats</Link></li>
                    <li><Link to="/home/groups">Groups</Link></li>
                </ul>
                <LogoutButton />
            </nav>

            <div style={{
                width: '80vw',
                //marginTop: '20px',
                //paddingTop: '20px'
            }}>
                <Outlet />
            </div>
        </div>
    );
};

export default HomeLayout;