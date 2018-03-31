﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPublisher
{
    public enum Events
    {
        RowSelectionChange,
        SetRightTextBoxText,
        ReOrder,
        Save,
        WordDocClosed,
        WordDocSaved,
        OpenWord,
        DescriptionCount,
        ShowFilterWindow,
        FilterDone,
        DataImportSelectionCompleted,
        StartDataImport,
        Undo,
        ReDo,
        Statistics,
        Formula,
        SetenceCountInDescription,
        SetenceCountInBullet,
        FindText,
        Relace,
        LoadTheme,
        WordsFrequency,
        RichTextBoxTextChanged,
        SearchTextInBackUp,
        MaximizeGridWindow,
        FindWindow,
        ShowHideColumns,
        ChangeBackGroundColor,
        ClearFilter,
        SpellCheck,
        CustomDictionaryUpdate,
        ToggleAutoSpellCheckMode
    }
}
