import './user.css'

export default function SwitchButton(props){

    return( <button onClick={props.onClick} className={props.isActive ? 'switch-btn btn-active' : 'switch-btn'}>
                {props.children}
            </button>
    )
}