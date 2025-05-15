import React, { useState, KeyboardEvent, ReactNode } from 'react';

interface AccordionProps {
    title: string;
    children: ReactNode;
}

const Accordion: React.FC<AccordionProps> = ({ title, children }) => {
    const [isOpen, setIsOpen] = useState(false);

    const toggleAccordion = () => {
        setIsOpen(!isOpen);
    };

    const handleButtonKeyDown = (e: KeyboardEvent<HTMLButtonElement>) => {
        if (e.key === 'Enter' || e.key === ' ') {
            e.preventDefault();
            toggleAccordion();
        }
    };

    return (
        <div className="accordion" >
            <div className="accordion-header" style={{ display: 'flex', alignItems: 'center' }}>
                <button
                    className="accordion-toggle"
                    onClick={toggleAccordion}
                    onKeyDown={handleButtonKeyDown}
                    aria-expanded={isOpen}
                    aria-controls="accordion-content"
                    style={{
                        color: '#646cff',
                        background: 'transparent',
                        cursor: 'pointer',
                        padding: '4px 8px',
                        marginRight: '8px',
                        fontSize: '1rem',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center'
                    }}
                >
                    {isOpen ? 'v' : '>'}
                </button>
                <h2 style={{ margin: 0, cursor: 'pointer' }} onClick={toggleAccordion}>
                    {title}
                </h2>
            </div>
            {isOpen && (
                <div
                    id="accordion-content"
                    className="accordion-content"
                    aria-hidden={!isOpen}
                    style={{ padding: '12px 0 0 28px' }}
                >
                    {children}
                </div>
            )}
        </div>
    );
};

export default Accordion;