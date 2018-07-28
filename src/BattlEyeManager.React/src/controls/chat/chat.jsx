import React, { Component } from 'react';
import PropTypes from 'prop-types';
import './chat.css';
import { InputMessage } from './inputMessage';

export class Chat extends Component {
    constructor(props) {
        super(props);

        this.chatBox = React.createRef();
    }

    componentDidUpdate() {
        var height = this.chatBox.current.scrollHeight;
        this.chatBox.current.scrollTop = height;
    }
   
    render() {
        const { items, newMessage } = this.props;
        return (
            <div>

                <div id="chatBox" ref={this.chatBox} className="bg-white rounded box-shadow">
                    {items && items.map && items.map((item, i) => <ChatItem key={i} item={item} />)}
                </div>  

                <InputMessage onSubmit={newMessage} />
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