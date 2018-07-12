import React from 'react';
import { MainMenu } from "./mainmenu";
import { Container, Row } from 'reactstrap';

export const PageTemplate = ({ children }) =>
    <div>        
        <MainMenu />
        <Container>
            <Row>
                {children}
            </Row>
        </Container>
    </div>