"""
update-license-headers.py adds license headers to all src files 
that don't already have them
Copyright (C) 2019  Ari Madian

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
"""

import os
import re
from datetime import datetime

license_text = """/*
{}
Copyright (C) {}  Ari Madian

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

"""

def doesFileHaveLicenseText(src):
    match = re.search(r"\/\*((\n(?:.+)\n)Copyright\*(?!\/)|[^*])*\*\/", str(src))
    if match:
        return True
    else:
        return False

for root, dirs, files in os.walk('../NZXTSharp/'):
    for name in files:
        if 'AssemblyInfo' in name:
            continue

        split_name = name.split('.')
        if split_name[-1] == 'cs':  # If file extension is .cs
            src_content = ""
            with open(os.path.join(root, name), 'r') as src:  # get src, insert license
                oline = src.readlines()
                if doesFileHaveLicenseText(oline):
                    print('File with license found -  {}... Skipping'.format(name))
                    continue

                formatted_licence_text = license_text.format(name, datetime.now().year)
                oline.insert(0, formatted_licence_text)
                src_content = oline

            with open(os.path.join(root, name), 'w') as src:  # overwrite file
                src.writelines(src_content)
