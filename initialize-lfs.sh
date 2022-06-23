track()
{
    git lfs track "$1"
}

append()
{
    echo $1
    echo $1 >> .gitattributes
}

if [ -f .gitattributes ] & [ -s .gitattributes ]; then 
    echo "Remove .gitattributes and try again!"
    exit 1
fi

append "## Unity ##"
append "\n"
append "*.cs diff=csharp text"
append "*.cginc text"
append "*.shader text"
append "\n"
append "*.mat merge=unityyamlmerge eol=lf"
append "*.anim merge=unityyamlmerge eol=lf"
append "*.unity merge=unityyamlmerge eol=lf"
append "*.prefab merge=unityyamlmerge eol=lf"
append "*.physicsMaterial2D merge=unityyamlmerge eol=lf"
append "*.physicMaterial merge=unityyamlmerge eol=lf"
append "*.meta merge=unityyamlmerge eol=lf"
append "*.controller merge=unityyamlmerge eol=lf"
append "\n"
track *.asset
append "\n"
append "## git-lfs ##"
append "\n"
append "#Image"
track *.jpg
track *.jpeg
track *.png
track *.gif
track *.psd
track *.ai
track *.bmp
track *.hdr
append "\n"
append "#Audio"
track *.mp3
track *.wav
track *.ogg
append "\n"
append "#Video"
track *.mp4
track *.mov
append "\n"
append "#3D Object"
track *.fbx
track *.blend
track *.obj
append "\n"
append "#ETC"
track *.a
track *.exr
track *.tga
track *.pdf
track *.zip
track *.dll
track *.unitypackage
track *.aif
track *.ttf
track *.rns
track *.reason
track *.lxo
track *.tgi
sed -i 's/\\n//g' .gitattributes