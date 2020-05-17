import { connect } from 'react-redux';
import { Action, Dispatch } from 'redux';
import { IdTextList } from 'src/controls';
import { IKickReason } from 'src/models';
import { kickReasonActions } from "../../store/actions";

const mapStateToProps = ({ kickReasons }: { kickReasons: any }) => {
    return {
        createUrl: "/kickReasons/create",
        editUrl: "/kickReasons/",
        error: kickReasons.error,
        items: kickReasons.items || [],
        title: "Kick reasons",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
    return {
        deleteItem: (item: IKickReason) => kickReasonActions.deleteItem(item)(dispatch),
        onLoad: () => kickReasonActions.getItems()(dispatch),
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextList);

export { ConnectedList as List };
