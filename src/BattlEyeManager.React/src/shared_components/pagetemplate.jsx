import React from 'react';
import { MainMenu } from "./mainmenu";
import { Container, Row, Col } from 'reactstrap';

export const PageTemplate = ({ children }) =>
    <div>
        <MainMenu />
        <Container>
            <Row>
                <Col xl="7" m="1">
                    {children}
                </Col>
            </Row>
        </Container>
    </div>
