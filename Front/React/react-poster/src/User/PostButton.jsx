import './user.css'

export default function PostButton(props){

    return( <button onClick={props.onClick} className='post-btn'>
                {props.children}
            </button>
    )
}