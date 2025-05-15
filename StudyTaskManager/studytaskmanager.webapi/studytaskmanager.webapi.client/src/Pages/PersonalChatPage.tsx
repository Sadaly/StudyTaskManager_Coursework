import { useParams } from 'react-router-dom';

function PersonalChatPage() {
    const { id } = useParams();

    return (
        <div>
            {/* Здесь будет отображение чата с id = {id} */}
            <h2>Personal Chat: {id}</h2>
        </div>
    );
}

export default PersonalChatPage;