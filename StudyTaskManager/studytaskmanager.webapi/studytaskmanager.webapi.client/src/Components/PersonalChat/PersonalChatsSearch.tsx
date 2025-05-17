import React, { useState } from "react";

interface PersonalChatsSearchProps {
}

const PersonalChatsSearch: React.FC<PersonalChatsSearchProps> = () => {
    const [searchUserName, setSearchUserName] = useState<string>("");

    const handleSearch = () => {
    };

    return (
        <>
            <input
                placeholder="search"
                value={searchUserName}
                onChange={(e) => setSearchUserName(e.target.value)}
                required
            />
            <button onClick={handleSearch}>Search</button>
        </>
    );
};

export default PersonalChatsSearch;