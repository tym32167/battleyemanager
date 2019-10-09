import moment from 'moment';
import React, { Component } from 'react';
import { IChatMessage } from 'src/models';
import './chat.css';
import { InputMessage } from './inputMessage';

export interface IChatProps {
    newMessage: (e: React.FormEvent<HTMLFormElement>) => void,
    items: IChatMessage[]
}

export class Chat extends Component<IChatProps> {
    private chatBox: React.RefObject<HTMLDivElement>;

    constructor(props: IChatProps) {
        super(props);
        this.chatBox = React.createRef();
    }

    public componentDidUpdate() {
        const height = this.chatBox.current!.scrollHeight;
        this.chatBox.current!.scrollTop = height;
    }

    public render() {
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

function getClassname(messageType: string) {
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

const ChatItem = ({ item }: { item: IChatMessage }) => (
    <div className="media text-muted @GetClass(item)">
        <p className={'media-body mb-0 small lh-125 ' + getClassname(item.type)}
            style={{ wordBreak: 'break-all' }}>
            {moment.utc(item.date).local().format('YYYY-MM-DD')}{' '}<strong>{moment.utc(item.date).local().format('HH:mm:ss')}{' '}</strong>
            {item.message}
        </p>
    </div>
);