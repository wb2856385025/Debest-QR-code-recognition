//-----------------------------------------------------------------------------------------
//File Name:UniDAQ.cs
//Update date:2014/05/14 by Dan Huang
//Support Language: Visual C#.NET 
//UniDAQ SDK User Manual: http://ftp.icpdas.com/pub/cd/iocard/pci/napdos/pci/unidaq/manual/
//-----------------------------------------------------------------------------------------

// TRANSINFO: Option Strict On
using System.Runtime.InteropServices; 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace UniDAQ_Ns
{
    // TRANSMISSINGCOMMENT: Class UniDAQ
    public class UniDAQ
    {
        public const uint MAX_BOARD_NUMBER = 32;
        public const uint MAX_EVENT_NUMBER  = 20;
        public const uint MAX_AO_CHANNEL  = 32;

        //Option Mode
        public const uint MULTIPROCESSOR = 1;
        public const uint ENABLEMEMIO = 2;
        public const uint ENABLEDMAAI = 16;

        //ModelNumber
        public const UInt32 PIOD56 =	    0x800140;
        public const UInt32 PEXD56 =        0x800140;
        public const UInt32 PIOD48 =	    0x800130;
        public const UInt32 PEXD48 =        0x800130;
        public const UInt32 PIOD64 =	    0x800120;
        public const UInt32 PIOD96 =	    0x800110;
        public const UInt32 PIOD144 =		0x800100;
        public const UInt32 PIOD168 =		0x800150;
        public const UInt32 PIODA =  	    0x800400;
        public const UInt32 PEXDA =         0x800400;
        public const UInt32 PIO821 = 	    0x800310;

        public const UInt32 PISOP16R16U =   0x1800FF;
        public const UInt32 PEXP16R16 =     0x1800FF;
        public const UInt32 PEXP8R8 =       0x1800FF;
        public const UInt32 PISOC64 =		0x800800;
        public const UInt32 PEXC64 =        0x800800;
        public const UInt32 PISOP64 =		0x800810;
        public const UInt32 PEXP64 =        0x800810;
        public const UInt32 PISOA64 =		0x800850;
        public const UInt32 PISOP32C32 =    0x800820;
        public const UInt32 PEXP32C32 =     0x800820;
        public const UInt32 PISOP32A32 =    0x800870;
        public const UInt32 PISOP8R8 =	    0x800830;
        public const UInt32 PISO730 =		0x800840;
        public const UInt32 PISO730A =	    0x800880;
        public const UInt32 PISO725 =		0x8008FF;
        public const UInt32 PISODA2 =		0x800B00;

        public const UInt32 PISO813 =		0x800A00;

        public const UInt32 PCITMC12 =	    0xDF2962;
        public const UInt32 PCIM1024 =	    0xDEA074;
        public const UInt32 PCIM512 =		0xDE9562;
        public const UInt32 PCIM256 =		0xDE92A6;
        public const UInt32 PCIM128 =		0xDE9178;
        public const UInt32 PCID64 =	    0xDE3513;
        public const UInt32 PCIFC16 =       0xB13017;
        public const UInt32 PCI822 =	    0xDE3823;
        public const UInt32 PCI826 =	    0xDE3827;
        public const UInt32 PCI2602 =       0x2CB656;

        public const UInt32 PCI100x =		0x341002;
        public const UInt32 PEX100x =       0x341002;
        public const UInt32 PCI1202 =		0x345672;
        public const UInt32 PEX1202 =       0x345672;
        public const UInt32 PCI1602 =		0x345676;
        public const UInt32 PCI180x =		0x345678;
        public const UInt32 PCIP8R8 =		0xD6102B;
        public const UInt32 PEXP8POR8 =     0xD6102B;
        public const UInt32 PCIP16R16 =	    0xD61E39;
        public const UInt32 PEXP16POR16 =   0xD61E39;

        //Return Code
        public const UInt16 Ixud_NoErr                  = 0;//Correct
        public const UInt16 Ixud_OpenDriverErr          = 1;//Open driver error
        public const UInt16 Ixud_PnPDriverErr           = 2;//Plug & Play error
        public const UInt16 Ixud_DriverNoOpen           = 3;//The driver was not open
        public const UInt16 Ixud_GetDriverVersionErr    = 4;//Recieve driver version error
        public const UInt16 Ixud_ExceedBoardNumber      = 5;//Board number error
        public const UInt16 Ixud_FindBoardErr           = 6;//No board found
        public const UInt16 Ixud_BoardMappingErr        = 7;//Board Mapping error
        public const UInt16 Ixud_DIOModesErr            = 8;//Digital input/output mode setting error
        public const UInt16 Ixud_InvalidAddress         = 9;//Invalid address
        public const UInt16 Ixud_InvalidSize            = 10;//Invalid size
        public const UInt16 Ixud_InvalidPortNumber      = 11;//Invalid port number
        public const UInt16 Ixud_UnSupportedModel       = 12;//This board model is not supported
        public const UInt16 Ixud_UnSupportedFun         = 13;//This function is not supported
        public const UInt16 Ixud_InvalidChannelNumber   = 14;//Invalid channel number
        public const UInt16 Ixud_InvalidValue           = 15;//Invalid value
        public const UInt16 Ixud_InvalidMode            = 16;//Invalid mode
        public const UInt16 Ixud_GetAIStatusTimeOut     = 17;//A timeout occurred while receiving the status of the analog input
        public const UInt16 Ixud_TimeOutErr             = 18;//Timeout error
        public const UInt16 Ixud_CfgCodeIndexErr        = 19;//A compatible configuration code table index could not be found
        public const UInt16 Ixud_ADCCTLTimeoutErr       = 20;//ADC controller a timeout error
        public const UInt16 Ixud_FindPCIIndexErr        = 21;//A compatible PCI table index value could not be found
        public const UInt16 Ixud_InvalidSetting         = 22;//Invalid setting value
        public const UInt16 Ixud_AllocateMemErr         = 23;//Error while allocating the memory space
        public const UInt16 Ixud_InstallEventErr        = 24;//Error while installing the interrupt event
        public const UInt16 Ixud_InstallIrqErr          = 25;//Error while installing the interrupt IRQ
        public const UInt16 Ixud_RemoveIrqErr           = 26;//Error while removing the interrupt IRQ
        public const UInt16 Ixud_ClearIntCountErr       = 27;//Error while the clear interrupt count
        public const UInt16 Ixud_GetSysBufferErr        = 28;//Error while retrieving the system buffer
        public const UInt16 Ixud_CreateEventErr         = 29;//Error while create the event
        public const UInt16 Ixud_UnSupportedResolution  = 30;//Resolution not supported
        public const UInt16 Ixud_CreateThreadErr        = 31;//Error while create the thread
        public const UInt16 Ixud_ThreadTimeOutErr       = 32;//Thread timeout error
        public const UInt16 Ixud_FIFOOverFlowErr        = 33;//FIFO overflow error
        public const UInt16 Ixud_FIFOTimeOutErr         = 34;//FIFO timeout error
        public const UInt16 Ixud_GetIntInstStatus       = 35;//Retrieves the status of the interrupt installation
        public const UInt16 Ixud_GetBufStatus           = 36;//Retrieves the status of the system buffer
        public const UInt16 Ixud_SetBufCountErr         = 37;//Error while setting the buffer count
        public const UInt16 Ixud_SetBufInfoErr          = 38;//Error while setting the buffer data
        public const UInt16 Ixud_FindCardIDErr          = 39;//Card ID code could not be found
        public const UInt16 Ixud_EventThreadErr         = 40;//Event Thread error
        public const UInt16 Ixud_AutoCreateEventErr     = 41;//Error while automatically creating an event
        public const UInt16 Ixud_RegThreadErr           = 42;//Register Thread error
        public const UInt16 Ixud_SearchEventErr         = 43;//Search Event error
        public const UInt16 Ixud_FifoResetErr           = 44;//Error while resetting the FIFO
        public const UInt16 Ixud_InvalidBlock           = 45;//Invalid EEPROM block
        public const UInt16 Ixud_InvalidAddr            = 46;//Invalid EEPROM address
        public const UInt16 Ixud_AcqireSpinLock         = 47;//Error while acquiring spin lock
        public const UInt16 Ixud_ReleaseSpinLock        = 48;//Error while releasing spin lock
        public const UInt16 Ixud_SetControlErr          = 49;//Analog input setting error
        public const UInt16 Ixud_InvalidChannels        = 50;//Invalid channel number
        public const UInt16 Ixud_SearchCardErr          = 51;//Invalid model number
        public const UInt16 Ixud_SetMapAddressErr       = 52;//Error while setting the mapping address
        public const UInt16 Ixud_ReleaseMapAddressErr   = 53;//Error while releasing the mapping address
        public const UInt16 Ixud_InvalidOffset          = 54;//Invalid memory offset
        public const UInt16 Ixud_ShareHandleErr         = 55 ;//Open the share memory fail
        public const UInt16 Ixud_InvalidDataCount       = 56 ;//Invalid data count
        public const UInt16 Ixud_WriteEEPErr            = 57 ;//Error while writing the EEPROM
        public const UInt16 Ixud_CardIOErr              = 58 ;//CardIO error
        public const UInt16 Ixud_IOErr                  = 59 ;//MemoryIO error

        public const UInt16 Ixud_SetScanChannelErr		= 60; //Set channel scan number error
        public const UInt16 Ixud_SetScanConfigErr       = 61; //Set channel scan config error
        public const UInt16 Ixud_GetMMIOMapStatus       = 62; //


        [StructLayout(LayoutKind.Sequential)]
        public struct IXUD_DEVICE_INFO
        {
            public UInt32 dwSize; //Structure Size
            public UInt16 wVendorID; //Vendor ID
            public UInt16 wDeviceID; //Device ID
            public UInt16 wSubVendorID; //Sub Vendor ID
            public UInt16 wSubDeviceID; //Sub Device ID
            // <VBFixedArray(5)> Public dwBAR() As UInt32
            public UInt32 dwBAR0; //PCI Bar 0
            public UInt32 dwBAR1; //PCI Bar 1
            public UInt32 dwBAR2; //PCI Bar 2
            public UInt32 dwBAR3; //PCI Bar 3
            public UInt32 dwBAR4; //PCI Bar 4
            public UInt32 dwBAR5; //PCI Bar 5 
            public byte BusNo;
            public byte DevNo;
            public byte IRQ;
            public byte Aux;
            // <VBFixedArray(5)> Public dwReserved1() As UInt32   'Reserver 
            public UInt32 dwBarVirtualAddress0; // PCI Bar Virtual Address Bar 0(For MMIO) 
            public UInt32 dwBarVirtualAddress1; // PCI Bar Virtual Address Bar 1(For MMIO)
            public UInt32 dwBarVirtualAddress2; // PCI Bar Virtual Address Bar 2(For MMIO) 
            public UInt32 dwBarVirtualAddress3; // PCI Bar Virtual Address Bar 3(For MMIO) 
            public UInt32 dwBarVirtualAddress4; // PCI Bar Virtual Address Bar 4(For MMIO) 
            public UInt32 dwBarVirtualAddress5; // PCI Bar Virtual Address Bar 5(For MMIO) 
            // Sub Initialize()
            //    ReDim dwBAR(5)
            //    ReDim dwReserved1(5)
            //  End Sub
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct IXUD_CARD_INFO
        {

            public UInt32 dwSize; // Structure size
            public UInt32 dwModelNo; // Model Number

            // CardID is update when calling the function each time.
            public byte CardID; // for new cards, 0xFF=N/A
            public byte wSingleEnded; // for new cards,1:S.E 2:D.I.F,0xFF=N/A
            public UInt16 wReserved; // Reserver

            public UInt16 wAIChannels; // Number of AI channels(AD)
            public UInt16 wAOChannels; // Number of AO channels(DA)

            public UInt16 wDIPorts; // Number of DI ports
            public UInt16 wDOPorts; // Number of DO ports

            public UInt16 wDIOPorts; // Number of DIO ports
            public UInt16 wDIOPortWidth; // The width is 8/16/32 bit.

            public UInt16 wCounterChannels; // Number of Timers/Counters
            public UInt16 wMomorySize; // PCI-M512==>512, Units in KB.
            public UInt32 dwReserved10; // Reserver 
            public UInt32 dwReserved11; // Reserver
            public UInt32 dwReserved12; // Reserver 
            public UInt32 dwReserved13; // Reserver 
            public UInt32 dwReserved14; // Reserver 
            public UInt32 dwReserved15; // Reserver

            // <VBFixedArray(5)> Public dwReserved1() As UInt32   'Reserver 
            // Sub Initialize()
            //     ReDim dwReserved1(5)
            // End Sub

        }


        public static IXUD_DEVICE_INFO sDevInfo;
        public static IXUD_CARD_INFO sCardInfo;

        public const uint MAX_CFGCODE_NUMBER = 24;//User definition the total number of the AI ConfigCodeTotal

        // User AI Config Code
        public const UInt16 IXUD_BI_20V = ((ushort)(23)); // //Bipolar +/- 20V
        public const UInt16 IXUD_BI_10V = ((ushort)(0)); // //Bipolar +/- 10V
        public const UInt16 IXUD_BI_5V = ((ushort)(1)); // //Bipolar +/-  5V
        public const UInt16 IXUD_BI_2V5 = ((ushort)(2)); // //Bipolar +/-  2.5V
        public const UInt16 IXUD_BI_1V25 = ((ushort)(3)); // //Bipolar +/-  1.25V
        public const UInt16 IXUD_BI_0V625 = ((ushort)(4)); // //Bipolar +/-  0.625V
        public const UInt16 IXUD_BI_0V3125 = ((ushort)(5)); // //Bipolar +/-  0.3125V
        public const UInt16 IXUD_BI_0V5 = ((ushort)(6)); // //Bipolar +/-  0.5V
        public const UInt16 IXUD_BI_0V05 = ((ushort)(7)); // //Bipolar +/-  0.05V
        public const UInt16 IXUD_BI_0V005 = ((ushort)(8)); // //Bipolar +/-  0.005V
        public const UInt16 IXUD_BI_1V = ((ushort)(9)); // //Bipolar +/-  1V
        public const UInt16 IXUD_BI_0V1 = ((ushort)(10)); // //Bipolar +/-  0.1V
        public const UInt16 IXUD_BI_0V01 = ((ushort)(11)); // //Bipolar +/-  0.01V
        public const UInt16 IXUD_BI_0V001 = ((ushort)(12)); // //Bipolar +/-  0.001V
        public const UInt16 IXUD_UNI_20V = ((ushort)(13)); // //Unipolar 0 ~ 20V
        public const UInt16 IXUD_UNI_10V = ((ushort)(14)); // //Unipolar 0 ~ 10V
        public const UInt16 IXUD_UNI_5V = ((ushort)(15)); // //Unipolar 0 ~  5V
        public const UInt16 IXUD_UNI_2V5 = ((ushort)(16)); // //Unipolar 0 ~  2.5V
        public const UInt16 IXUD_UNI_1V25 = ((ushort)(17)); // //Unipolar 0 ~  1.25V
        public const UInt16 IXUD_UNI_0V625 = ((ushort)(18)); // //Unipolar 0 ~  0.625V
        public const UInt16 IXUD_UNI_1V = ((ushort)(19)); // //Unipolar 0 ~  1V
        public const UInt16 IXUD_UNI_0V1 = ((ushort)(20)); // //Unipolar 0 ~  0.1V
        public const UInt16 IXUD_UNI_0V01 = ((ushort)(21)); // //Unipolar 0 ~  0.01V
        public const UInt16 IXUD_UNI_0V001 = ((ushort)(22)); // //Unipolar 0 ~  0.001V


        public const uint MAX_AOCFGCODE_NUMBER = 6;//User definition the total number of the AO ConfigCodeTotal
        //User AO Config Code for Voltage
        public const UInt16 IXUD_AO_UNI_5V = ((ushort)(0));       //Unipolar 0  ~  5V
        public const UInt16 IXUD_AO_BI_5V = ((ushort)(1));        //Bipolar  +/-   5V
        public const UInt16 IXUD_AO_UNI_10V = ((ushort)(2));     //Unipolar 0  ~ 10V
        public const UInt16 IXUD_AO_BI_10V = ((ushort)(3));       //Bipolar +/-  10V
        public const UInt16 IXUD_AO_UNI_20V = ((ushort)(4));      //Unipolar 0  ~ 20V
        public const UInt16 IXUD_AO_BI_20V = ((ushort)(5));       //Bipolar +/-  20V
 
        //User AO Config Code for Current
        public const UInt16 IXUD_AO_I_0_20MA = ((ushort)(16));     //0 ~ 20mA
        public const UInt16 IXUD_AO_I_4_20MA = ((ushort)(17));     //4 ~ 20mA

        //Interrupt Setting
        public const UInt16 IXUD_HARDWARE_INT = ((ushort)(1)); // Hardware Interrupt
        public const UInt16 IXUD_APC_READY_INT = ((ushort)(2)); //APC Ready Interrupt
        public const UInt16 IXUD_ACTIVE_LOW = ((ushort)(4));  // Active low Trigger
        public const UInt16 IXUD_ACTIVE_HIGH = ((ushort)(8));   // Active high Trigger

        //Analog Trigger Mode Setting
        public const UInt16 IXUD_ANALOGTRIGGER_ABOVE  = 1; //Above high
        public const UInt16 IXUD_ANALOGTRIGGER_BELOW  = 2; //Below low
        public const UInt16 IXUD_ANALOGTRIGGER_LEAVE = 3; //Leave region
        public const UInt16 IXUD_ANALOGTRIGGER_ENTRY = 4; //Entry region
           
        public delegate void CallBackFun(UInt32 Param);
                
        // TRANSMISSINGCOMMENT: Method ZeroMemory
        [DllImport("kernel32.dll")]
        public static extern void ZeroMemory(IntPtr addr, int size);


        //  The Driver functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetDllVersion(ref UInt32 dwDLLver);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_OptionMode(UInt32 dwMode);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_DriverInit(ref UInt16 wTotalBoard);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_DriverClose();

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SearchCard(ref UInt16 wTotalBoard ,UInt32 dwModelNumber);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetBoardNoByCardID(ref UInt16 wBoardNo, UInt32 dwModelNumber, UInt16 wCardID);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetCardInfo(UInt16 wBoardNo, ref IXUD_DEVICE_INFO sDevInfo, ref IXUD_CARD_INFO sCardInfo, [MarshalAs(UnmanagedType.LPArray)]byte[] szModelName);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadPort(UInt32 dwAddress, UInt16 wsize, ref UInt32 dwVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WritePort(UInt32 dwAddress, UInt16 wsize, UInt32 dwVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadPort32(UInt32 dwAddress, ref UInt32 dwLow, ref UInt32 dwHigh);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WritePort32(UInt32 dwAddress, UInt32 dwLow, UInt32 dwHigh);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadPhyMemory(UInt32 dwAddress, UInt16 wSize, ref UInt32 dwVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WritePhyMemory(UInt32 dwAddress, UInt16 wSize, UInt32 dwVal);


        //Digital I/O functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SetDIOModes32(UInt16 wBoardNo, UInt32 dwDirections);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SetDIOMode(UInt16 wBoardNo, UInt16 wPortNo, UInt16 wDirection);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadDI(UInt16 wBoardNo, UInt16 wPortNo, ref UInt32 dwDIVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadDIBit(UInt16 wBoardNo, UInt16 wPortNo, UInt16 wBitNo, ref UInt16 wDIVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteDO(UInt16 wBoardNo, UInt16 wPortNo, UInt32 dwDOVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteDOBit(UInt16 wBoardNo, UInt16 wPortNo, UInt16 wBitNo, UInt16 wDOVal);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SoftwareReadbackDO(UInt16 wBoardNo, UInt16 wPortNo, ref UInt32 dwDOVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16  Ixud_StartDO(UInt16 wBoardNo, UInt16 wPortNo,UInt32 dwMode, float fSamplingRate, UInt32 dwDataCount,UInt32 dwCycleNum, UInt32[] dwDOVal);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16  Ixud_StopDO(UInt16 wBoardNo,UInt16 wPortNo);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16  Ixud_StartDI(UInt16 wBoardNo, UInt16 wPortNo,UInt32 dwMode, float fSamplingRate, UInt32 dwDataCount);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16  Ixud_StopDI(UInt16 wBoardNo,UInt16 wPortNo);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16	Ixud_GetDIBufferH(UInt16 wBoardNo,UInt16 wPortNo,UInt32 dwDataCount,UInt32[] hValue);
        
        //Callback functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SetEventCallback(UInt16 wBoardNo, UInt16 wEventType, UInt16 wIntSource, ref UInt32 hEvent, CallBackFun CallbackFunction, UInt32 dwCallbackParameter);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_RemoveEventCallback(UInt16 wBoardNo, UInt16 wIntSource);

        //Interrupt functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_InstallIrq(UInt16 wBoardNo, UInt32 dwIntMask);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_RemoveIrq(UInt16 wBoardNo);

        //Timer/Counter functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadCounter(UInt16 wBoardNo, UInt16 wChannelNo, ref UInt32 dwVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_SetCounter(UInt16 wBoardNo, UInt16 wChannelNo, UInt16 wMode, UInt32 dwVal);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_DisableCounter(UInt16 wBoardNo, UInt16 wChannelNo);

        //Memory I/O functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadMemory(UInt16 wBoardNo, UInt32 dwOffsetByte, UInt16 Size, ref UInt32 dwValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteMemory(UInt16 wBoardNo, UInt32 dwOffsetByte, UInt16 Size, UInt32 dwValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadMemory32(UInt16 wBoardNo, UInt32 dwOffsetByte, ref UInt32 dwLow, ref UInt32 dwHigh);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteMemory32(UInt16 wBoardNo, UInt32 dwOffsetByte, UInt32 dwLow, UInt32 dwHigh);
        
        //Analog Input functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadAI(UInt16 wBoardNo, UInt16 wChannel, UInt16 wConfig, ref float fValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ReadAIH(UInt16 wBoardNo, UInt16 wChannel, UInt16 wConfig, ref UInt32 dwValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ConfigAI(UInt16 wBoardNo, UInt16 wFIFOSizeKB, UInt32 BufferSizeKB, UInt16 wCardType, UInt16 wDelaySettlingTime);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ConfigAIEx(UInt16 wBoardNo, UInt16 wFIFOSizeKB, UInt32 BufferSizeCount, UInt16 wCardType, UInt16 wDelaySettlingTime, UInt32 dwMode);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_PollingAI(UInt16 wBoardNo, UInt16 wChannel, UInt16 wConfig, UInt32 dwDataCount, float[] fAIBuf);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_PollingAIH(UInt16 wBoardNo, UInt16 wChannel, UInt16 wConfig, UInt32 dwDataCount, UInt32[] dwAIBuf);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_PollingAIScan(UInt16 wBoardNo, UInt16 wChannel, UInt16[] wChannelList, UInt16[] wConfigList, UInt32 dwDataPreChCount, float[] fAIScanBuf);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_PollingAIScanH(UInt16 wBoardNo, UInt16 wChannel, UInt16[] wChannelList, UInt16[] wConfigList, UInt32 dwDataPreChCount, UInt32[] dwAIScanBuf);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ClearAIBuffer(UInt16 wBoardNo);
        
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetBufferStatus(UInt16 wBoardNo, ref UInt16 wBufferStatus, ref UInt32 dwDataCount);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_StartAI(UInt16 wBoardNo, UInt16 wChannel, UInt16 wConfig, float fSamplingRate, UInt32 dwDataCount);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_StartAIScan(UInt16 wBoardNo, UInt16 wTotalChannels, UInt16[] wChannelList, UInt16[] wConfigList, float fSamplingRate, UInt32 dwDataCount);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetAIBuffer(UInt16 wBoardNo, UInt32 dwDataCount, float[] fValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_GetAIBufferH(UInt16 wBoardNo, UInt32 dwDataCount, UInt32[] hValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_StopAI(UInt16 wBoardNo);

        //Analog Output functions
        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_ConfigAO(UInt16 wBoardNo, UInt16 wChannel, UInt16 wCfgCode);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteAOVoltage(UInt16 wBoardNo, UInt16 wChannel, float fValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteAOVoltageH(UInt16 wBoardNo, UInt16 wChannel, UInt16 hValue);

        [DllImport("UniDAQ.dll")]
        public static extern UInt16 Ixud_WriteAOCurrent(UInt16 wBoardNo, UInt16 wChannel, float fValue);

        [DllImport("UniDAQ.dll")]
        internal static extern UInt16 Ixud_WriteAOCurrentH(UInt16 wBoardNo, UInt16 wChannel, UInt16 hValue);    
    
        [DllImport("UniDAQ.dll")]
        internal static extern UInt16 Ixud_StartAOVoltage(UInt16 wBoardNo, UInt16 wChannel,UInt16 wConfig, float fSamplingRate, UInt32 dwDataCount,UInt32 dwCycleNum, float[] fValue);
        
        [DllImport("UniDAQ.dll")]
        internal static extern UInt16  Ixud_StartAOVoltageH(UInt16 wBoardNo, UInt16 wChannel,UInt16 wConfig, float fSamplingRate, UInt32 dwDataCount,UInt32 dwCycleNum, UInt32[] dwValue);
        
        [DllImport("UniDAQ.dll")]
        internal static extern UInt16  Ixud_StopAO(UInt16 wBoardNo, UInt16 wChannel);
       
    }



}