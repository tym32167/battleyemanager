import React, { Component } from 'react';
import { Form, Input, Button } from 'reactstrap';
import PropTypes from 'prop-types';
import './chat.css';

export class Chat extends Component {
    constructor(props) {
        super(props);

        this.chatBox = React.createRef();
        this.inpBox = React.createRef();
        this.sendNewMessageClick = this.sendNewMessageClick.bind(this);
    }

    componentDidUpdate() {
        var height = this.chatBox.current.scrollHeight;
        this.chatBox.current.scrollTop = height;
    }

    sendNewMessageClick(e){
        e.preventDefault();        
        const {value} = this.inpBox;
        const { newMessage } = this.props;        

        if (value && value !== '' && newMessage){
            newMessage && newMessage(value);
        }
    }

    render() {
        const { items } = this.props;
        return (
            <div>

                <div id="chatBox" ref={this.chatBox} className="bg-white rounded box-shadow">
                    {items && items.map && items.map((item, i) => <ChatItem key={i} item={item} />)}
                </div>
                
                <Form inline className="mt-1" onSubmit={this.sendNewMessageClick} >
                    <Input innerRef={v=>this.inpBox=v} className="col-sm" placeholder="Chat Message"></Input>
                    <Button color="primary">Send</Button>
                </Form>

            </div>
        );
    }
}

function getClassname(messageType) {
    switch (messageType) {
        case 'Vehicle':
        case 'Command':
            return "table-warning";
        case 'Group':
            return "table-success";
        case 'Side':
            return "table-info";
        case 'Direct':
            return "table-active";
        case 'RCon':
            return "table-danger";
        case 'Global':
            return '';
        default:
            return '';
    }
}

const ChatItem = ({ item }) => (
    <div className="media text-muted @GetClass(item)">
        <p className={'media-body mb-0 small lh-125 ' + getClassname(item.type)}>
            <strong>{new Date(item.date).toLocaleTimeString()}{' '}</strong>
            {item.message}
        </p>
    </div>
)


Chat.propTypes = {
    items: PropTypes.array,
    newMessage: PropTypes.func
}

ChatItem.propTypes = {
    item: PropTypes.object
}