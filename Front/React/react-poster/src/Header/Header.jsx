import './header.css'

export default function Header(props){
    return(
        <header className='hdr'>{props.children}</header>
    )
}