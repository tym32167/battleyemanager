import React from 'react';
import { MainMenu } from "./mainmenu";
import { Container, Row } from 'reactstrap';
import { Route } from "react-router-dom";

export const PageTemplate = ({ children }) =>
    <div>        
        <Route  component={MainMenu} />
        <Container>
            <Row>
                {children}
            </Row>
        </Container>
    </div>