import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IdTextList } from 'src/controls';
import { IKickReason } from 'src/models';
import { kickReasonActions } from "../../store/actions";

const mapStateToProps = ({ kickReasons }: { kickReasons: any }) => {
    return {
        createUrl: "/kickReasons/create",
        editUrl: "/kickReasons/",
        error: kickReasons.error,
        items: kickReasons.items || [],
        title: "Kick reasons:",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        deleteItem: (item: IKickReason) => dispatch(kickReasonActions.deleteItem(item)),
        onLoad: () => dispatch(kickReasonActions.getItems()),
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextList);

export { ConnectedList as List };
